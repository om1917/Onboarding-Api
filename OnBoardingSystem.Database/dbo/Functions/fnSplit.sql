﻿CREATE FUNCTION [dbo].[fnSplit]  
(  
@List nvarchar(max),  
@SplitOn nvarchar(5)  
)   
RETURNS @RtnValue table   
(   
Id int identity(1,1),  
Value nvarchar(200)  
)   
AS   
BEGIN   
While (Charindex(@SplitOn,@List)>0)  
Begin   
Insert Into @RtnValue (value)  
Select   
Value = ltrim(rtrim(Substring(@List,1,Charindex(@SplitOn,@List)-1)))   
  
Set @List = Substring(@List,Charindex(@SplitOn,@List)+len(@SplitOn),len(@List))  
End   
  
Insert Into @RtnValue (Value)  
Select Value = ltrim(rtrim(@List))  
  
Return  
END