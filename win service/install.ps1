$path=Join-Path -Path $PSScriptRoot -ChildPath "..\Service.exe" | Resolve-Path
New-Service MyService -BinaryPathName $path