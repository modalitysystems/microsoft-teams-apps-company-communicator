# Deploying Reporting Additions

This document provides guidance for deploying the reporting additions made to the Company Communicator application. It assumes an existing deployment of Company Communicator.

## First Install

1. Create a new Azure Function App. The choice of Consumption Plan vs Dedicated Instance is not important to how the application operates, however any chosen configuration should be capable of supporting the load expected, roughly in line with the load seen on the Send Function app.

2. Deploy the code from the /Source/Microsoft.Teams.Apps.CompanyCommunicator.ReportImage.Func/ folder to the newly created Azure Function app.
3. Add the following Application Settings:
  * Name: I18n:DefaultCulture Value: en-US
  * Name: IsItExpectedThatTableAlreadyExists Value: false
  * Name: StorageAccountConnectionString Value: *connection string to existing storage account in Company Communicator Resource Group*
4. Click Save to store the new Application Settings.
5. Using the left-hand menu bar, view the Functions in the Function App. Select the ReportCardViewFunction, and then select "Get Function URL" from the top menu. Copy the default (function key) URL. It should look something like: https://modalitycommunicator-reportingimage-function.azurewebsites.net/api/ReportCardViewFunction?code=vpF4Js...i8Kd4NNkw==
6. Navigate to the Send Function App. Add a new Application Setting:
  * Name: ReportingFunctionUrl Value: *paste the URL of the Function from the previous step*
7. Deploy the code from the /Source/CompanyCommunicator.Send.Func/ folder to the Send Function app.

Once deployment has completed and at least one Company Communicator message has been sent out, the Application Setting *IsItExpectedThatTableAlreadyExists* can be set to True for improved performance.

## Subsequent Updates

It is likely that Company Communicator will evolve over time as bug fixes and enhancements are implemented. Any standard deployment of Company Communicator using the ARM template will overwrite the changes made above. To address this, create a local branch of Company Communicator that incorporates the changes made for Reporting, link this to the Azure Function in Deployment Center and then periodically accept changes from the main fork as needed.

Alternatively, to retrospectively apply the changes needed for reporting, it is only nessecary to update the Send Function with the changes in this branch. There won't be any changes to the Reporting Function as it is a new functon created for reporting and as such will never be updated via a change from the parent fork.
