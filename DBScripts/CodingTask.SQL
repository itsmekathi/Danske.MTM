-- Requirement analysis and conclusion.
As per the description and my understanding, there are three entities here
1, tax types -> yearly, monthly, weekly, daily.
2, Municipality -> Municipality name.
3, Municipality tax schedule - the various scheduled taxes for the municipality entries.

Assumptions
> Municipality name should be unique and case insensitive, with minimum length of 5 and max length of 200.
> Assuming that tax cannot be negative or zero.
> Year starts at the first day of January and ends in last day of December.
> Week starts on monday and ends on sunday.

Design Decisions
> Logging will be done at an API level for exceptions as API would be the client facing part.

-- Data assumtions
> There cannot be duplicate entry in Municipality Tax Schedule(eg: There cannot be daily taxes scheduled for the same day more than once, similar criteria applies for remaining types).


-- Database Schema design and Normalization.

TaxScheduleTypes 
>ID, NAME

Municipality
> ID, Name

For the sake of simplicity we will keep the municipality name in the municipality taxes table itself as it would be an optimal solution and would reduce one join condition while fetching records.

Municipality Tax Schedule
> ID,MunicipalityName, TaxScheduleTypeID, FromDate, ToDate, CreatedOn, ModifiedOn


