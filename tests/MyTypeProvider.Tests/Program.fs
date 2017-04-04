module MyTypeProvider.Tests

open Expecto

type TP45 = MyTypeProvider.Provided.MyType

let tests =
  testList "MyTP" [
    test "profile259" {
      let actual =
        try
            Library.Profile259.getValue()
        with
        | e -> failwithf "Ooops %A" e
      Expect.stringContains actual ".Runtime.dll" "Function call should be rewritten to .Runtime.dll"
    }

    test "net45" {
      let actual =
        try
            TP45.MyProperty
        with
        | e -> failwithf "Ooops %A" e
      Expect.stringContains actual ".Runtime.dll" "Function call should be rewritten to .Runtime.dll"
    }
  ]

[<EntryPoint>]
let main args =
  let config =
    { defaultConfig with
        verbosity = Logging.LogLevel.Verbose }
  runTestsWithArgs config args tests
