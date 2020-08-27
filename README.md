# Solution for Round 2 interview of DANSKE :computer:
Contains Solution for Coding round interview for DANSKE IT.

## Problem description and interpretation
>_As per the description and my understanding, there are three entities/domain objects_.
1. Tax types -> yearly, monthly, weekly, daily.
2. Municipality -> Municipality name.
3. Municipality tax schedule - the various scheduled taxes for the municipality entries.


## Assumptions
1. Municipality name should be unique and case insensitive, with minimum length of 5 and max length of 200.
2. Assuming that tax cannot be negative or zero.
3. Year starts at the first day of January and ends in last day of December.
4. Week starts on monday and ends on sunday.

## Pre-requisites for running the code
* .NET core 3.1 or latest should be installed on the machine.
* Microsoft Visual Studio Professional 2019 Version 16.4.2 or latest
* SQL Server 2008 and upwards.


## Database Schema design and Normalization.
Below tables would be needed to implement the requirement

* TaxScheduleTypes (ID, NAME)
* Municipality (ID, Name)
* Municipality Tax Schedules (ID,MunicipalityName, TaxScheduleTypeID, FromDate, ToDate, TaxAmount ,CreatedOn, ModifiedOn)

> _For the sake of simplicity we will keep the municipality name in the municipality taxes table itself as it would be an optimal solution and would reduce one join condition while fetching records_.


## Design Decisions
* The solution would be implemented and available as a set of web api endpoints.
* Logging will be done at an API level for exceptions as API would be the client facing part.

## Application Logic
> * From the description provided, it is clear that the taxes scheduled for day is of higher precedence and then weekly and similary month and year, so this logic will be implemented in the service layer as as a set of if statements if data exists for day, then week and so on.
> * All data which are present in the DB should be created by API.
> * Validations would be added at the API front as well as database constraints.

## Instructions for Running the project locally.
1. Run the scripts provided in folder DBScripts in the same order as they are numbered.
2. Verify that the startup project is set as Danske.MTM.WebApi.
3. Check the environment you are in, this would be visible checking the project setting of Danske.MTM.WebApi, debug window, environment variables.
4. Configure the Connection string in app.{environment}.json to point to the created database.
5. Start running the project with kestral with or without debugging.

## Web Links
* [All pending tasks](https://github.com/itsmekathi/Danske.MTM/issues)
* [Pending Development tasks](https://github.com/itsmekathi/Danske.MTM/issues?q=is%3Aopen+is%3Aissue+label%3Adevelopment)
* [Pending Enhancements](https://github.com/itsmekathi/Danske.MTM/issues?q=is%3Aopen+is%3Aissue+label%3Aenhancement)