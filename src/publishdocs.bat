mkdir pubdocs
cd pubdocs

git clone https://github.com/SkillsFundingAgency/das-paymentsacceptancetesting.git --branch gh-pages

xcopy ..\das-payments-AcceptanceTests\AcceptanceTests\docs das-paymentsacceptancetesting /E /R /Y

cd das-paymentsacceptancetesting
git add --all
git commit -m "Update docs for release %RELEASE_RELEASENAME%"
git push origin gh-pages