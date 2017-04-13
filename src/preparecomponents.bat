mkdir ..\..\..\workingdir
mkdir ..\..\..\workingdir\components

xcopy ..\..\..\das-collectionearnings-opa-calculator ..\..\..\workingdir\components /E
xcopy ..\..\..\das-collectionearnings-datalock ..\..\..\workingdir\components /E
xcopy ..\..\..\das-providerpayments-calculator ..\..\..\workingdir\components /E

xcopy ..\..\..\das-payment-reference-commitments ..\..\..\workingdir\components /E
xcopy ..\..\..\das-payment-reference-accounts ..\..\..\workingdir\components /E
xcopy ..\..\..\das-providerevents-components ..\..\..\workingdir\components /E