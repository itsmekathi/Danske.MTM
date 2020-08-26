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

## Database Schema design and Normalization.
Below tables would be needed to implement the requirement

* TaxScheduleTypes (ID, NAME)
* Municipality (ID, Name)
* Municipality Tax Schedules (ID,MunicipalityName, TaxScheduleTypeID, FromDate, ToDate, CreatedOn, ModifiedOn)

> _For the sake of simplicity we will keep the municipality name in the municipality taxes table itself as it would be an optimal solution and would reduce one join condition while fetching records_.


## Design Decisions
* Logging will be done at an API level for exceptions as API would be the client facing part.

## Instructions for Running the project locally.
__TO_BE_ADDED__

## Web Links
__TO_BE_ADDED__