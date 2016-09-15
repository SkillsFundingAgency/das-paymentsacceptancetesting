packages\NUnit.ConsoleRunner.3.4.1\tools\nunit3-console.exe SFA.DAS.Payments.AcceptanceTests\bin\Debug\SFA.DAS.Payments.AcceptanceTests.dll --result=SFA.DAS.Payments.AcceptanceTests\bin\Debug\TestResult.xml

rm ..\docs
mkdir ..\docs
packages\Pickles.CommandLine.2.8.2\tools\pickles.exe --feature-directory ..\features --link-results-file SFA.DAS.Payments.AcceptanceTests\bin\Debug\TestResult.xml --test-results-format nunit3 --output-directory ..\docs