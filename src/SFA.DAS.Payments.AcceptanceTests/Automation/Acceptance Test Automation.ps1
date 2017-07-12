#xip file

$zip = Test-Path C:\inetpub\wwwroot\ftproot\Payments.zip

If ($zip -eq "True")
{
Write-host "Zip File is here checking if tests are running"
}
else
{
exit
}

Get-ChildItem C:\inetpub\wwwroot\ftproot\Payments.zip | % {& "C:\Program Files\7-Zip\7z.exe" "x" $_.fullname "-oC:\inetpub\wwwroot\ftproot\Payments"}

Remove-Item C:\inetpub\wwwroot\TestResults.xml -ErrorAction SilentlyContinue

#Create File to Block it being Re-Ran


$Running = Test-Path C:\inetpub\wwwroot\RunningTests.html

If ($Running -eq "True"){

Write-Host "Already Running Acceptance Test"
}

else 

{
New-Item C:\inetpub\wwwroot\RunningTests.html -ItemType File
cd 'C:\inetpub\wwwroot\FTPRoot\Payments\das-payments-acceptancetests\AcceptanceTests\src'
cmd.exe /C "C:\inetpub\wwwroot\FTPRoot\Payments\das-payments-acceptancetests\AcceptanceTests\src\preparecomponents.bat"

cd "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow"

#Runs Test via VSTEST
cmd.exe /C "vstest.console.exe C:\inetpub\wwwroot\FTProot\Payments\das-payments-acceptancetests\AcceptanceTests\src\SFA.DAS.Payments.AcceptanceTests\bin\Release\SFA.DAS.Payments.AcceptanceTests.dll /Tests:_581_AC01_NonDASLearnerFinishesEarlyPriceEqualsTheFundingBandMaximumEarnsBalancingAndCompletionFrameworkUpliftPayments_Assumes15MonthApprenticeshipAndLearnerCompletesAfter12Months_ /UseVsixExtensions:true /Logger:trx"

#Tests complete remove holding page
Remove-Item C:\inetpub\wwwroot\RunningTests.html -Recurse -Force

#Create 
New-Item C:\inetpub\wwwroot\RunningComplete.html -ItemType File

Move-Item "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\TestResults\*.trx" "C:\inetpub\wwwroot\TestResults.trx" 

start-Sleep -s 360
cd 'c:\inetpub'
Remove-Item C:\inetpub\wwwroot\RunningComplete.html -Force 
Remove-item C:\inetpub\wwwroot\testresults.trx -Force
Remove-Item C:\inetpub\wwwroot\FTPRoot\payments -Recurse -Force -ErrorAction SilentlyContinue 
Remove-Item C:\inetpub\wwwroot\FTPRoot\payments.zip -Recurse -Force -ErrorAction SilentlyContinue
exit
}
