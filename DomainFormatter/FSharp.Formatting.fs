﻿module FSharp.Formatting

// This is logging setup and might not be needed.
// Copied from "FSharp.Formatting.fsx"
// but not working in .NET Core :(


(*
// Force load
if (typeof<System.Web.Razor.ParserResults>.Assembly.GetName().Version.Major <= 2) then
  failwith "Wrong System.Web.Razor Version loaded!"

// Setup Logging for FSharp.Formatting and Yaaf.FSharp.Scripting
module Logging = FSharp.Formatting.Common.Log
type TraceOptions = System.Diagnostics.TraceOptions

// By default, we log to console only. Other modes are enabled by setting
// the `FSHARP_FORMATTING_LOG` environment variable.
let logToFile, logToConsole =
  match System.Environment.GetEnvironmentVariable("FSHARP_FORMATTING_LOG") with
  | "ALL" -> true, true
  | "NONE" -> false, false
  | "FILE_ONLY" -> true, false
  | _ -> false, true

try
  let allTraceOptions =
    TraceOptions.Callstack ||| TraceOptions.DateTime ||| TraceOptions.LogicalOperationStack |||
    TraceOptions.ProcessId ||| TraceOptions.ThreadId ||| TraceOptions.Timestamp
  let noTraceOptions = TraceOptions.None
  let svclogFile = "FSharp.Formatting.svclog"
  System.Diagnostics.Trace.AutoFlush <- true

  let setupListener listener =
    [ FSharp.Formatting.Common.Log.source
      Yaaf.FSharp.Scripting.Log.source ]
    |> Seq.iter (fun source ->
        source.Switch.Level <- System.Diagnostics.SourceLevels.All
        Logging.AddListener listener source)

  if logToConsole then
    Logging.ConsoleListener()
    |> Logging.SetupListener noTraceOptions System.Diagnostics.SourceLevels.Verbose
    |> setupListener

  if logToFile then
    if System.IO.File.Exists svclogFile then System.IO.File.Delete svclogFile
    Logging.SvclogListener svclogFile
    |> Logging.SetupListener allTraceOptions System.Diagnostics.SourceLevels.All
    |> setupListener

  // Test that everything works
  Logging.infof "FSharp.Formatting Logging setup!"
  Yaaf.FSharp.Scripting.Log.infof "Yaaf.FSharp.Scripting Logging setup!"
with e ->
  printfn "FSharp.Formatting Logging setup failed: %A" e


*)