namespace MyTypeProvider.DesignTime

open System.Reflection
open System.IO
open ProviderImplementation
open ProviderImplementation.ProvidedTypes
open ProviderImplementation.ProvidedTypesTesting
open Microsoft.FSharp.Core.CompilerServices


[<TypeProvider>]
type GenerativePropertyProviderWithStaticParams (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces ()

    let ns = "MyTypeProvider.Provided"
    let asm = Assembly.GetExecutingAssembly()
    let tempAssembly =
        Path.ChangeExtension(Path.GetTempFileName(), "dll")
        |> ProvidedAssembly

    do
        let myType = ProvidedTypes.ProvidedTypeDefinition(asm, ns, "MyType", Some typeof<obj>, IsErased=false)
        let myProp = ProvidedTypes.ProvidedProperty("MyProperty", typeof<string>, IsStatic = true,
                            GetterCode = (fun args -> <@@ MyTypeProvider.Runtime.Impl.myFunc() @@>))
        myType.AddMember(myProp)

        tempAssembly.AddTypes [myType]
        this.AddNamespace(ns, [myType])
