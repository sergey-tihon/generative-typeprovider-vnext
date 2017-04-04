module MyTypeProvider.Runtime.Impl

let myFunc () =
#if RUNTIME_ASSEMBLY
    "Value from MyTypeProvider.Runtime.dll"
#else
    "Value from MyTypeProvider.DesignTime.dll"
#endif