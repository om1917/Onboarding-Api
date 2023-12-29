CREATE PROCEDURE [dbo].[sp_GetApiLink]  
AS    
Begin    
    Select distinct X.AgencyId,X.AgencyName,A.apiLink,a.appYear
	from  Zmst_ApplicationSummary A
	INNER JOIN  Zmst_Agency X on A.AgencyId=X.AgencyId
	INNER JOIN (
	Select AgencyId,Max(appYear) appYear from Zmst_ApplicationSummary
	where appYear>=2022
	group by AgencyId
	)SS On A.AgencyId=SS.AgencyId and A.appYear=Ss.appYear
	where ss.appYear>=2022 and a.status<>'N'
	order by appYear desc,
	AgencyName asc 
End