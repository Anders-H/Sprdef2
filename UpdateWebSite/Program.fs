open System
open System.IO
open System.Text

let templateFilename = "D:\\GitRepos\\Sprdef2\\Sprdef2\\web\\index.html.template"
let outputFilename = "D:\\GitRepos\\Sprdef2\\Sprdef2\\web\\index.html"

let versionHistory = @"
<li><strong>1.6:</strong> A new flood fill tool, a new sprite picker, a recent file list and new keyboard shortcuts.</li>
<li><strong>1.5:</strong> A new rectangle tool and a new circle tool.</li>
<li><strong>1.4:</strong> Export to prg file for Commodore 64/128 and D64 image file.</li>
<li><strong>1.3:</strong> CBM Prg Studio export, a few minor bug fixes, a new line tool and undo/redo functionality.</li>
<li><strong>1.2:</strong> Free hand editing, better sprite preview options and fixed an export bug. Use right mouse button to delete a pixel.</li>
<li><strong>1.1:</strong> New keyboard shortcuts, and added animation view.</li>
"

let constants = 
    [
        "{{VersionHistory}}", versionHistory
        "{{LastUpdated}}", "2026-06-19"
        "{{DATUM}}", DateTime.Now.ToString("yyyy-MM-dd")
    ] |> Map.ofList

[<EntryPoint>]
let main argv =
    printfn "Loading template data."

    if File.Exists(templateFilename) then
        let originalText = File.ReadAllText(templateFilename)

        let uppdatedText = 
            constants 
            |> Map.fold (fun (textAcc: string) placeholder value -> 
                textAcc.Replace(placeholder, value)) originalText

        File.WriteAllText(outputFilename, uppdatedText, Encoding.UTF8)
            
        printfn "HTML is written to '%s'." outputFilename
    else
        printfn "Template filename '%s' not found." templateFilename

    0
