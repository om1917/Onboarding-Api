CREATE procedure RefreshToken          
@UserId varchar(100),        
@RefreshToken varchar(500),        
@token varchar(1000),      
@Mode varchar(10)      
As          
Begin      
 if(@Mode='login')      
 Begin   
  Insert into User_Authorization (userId,refreshToken,Token) values (@UserId,@RefreshToken,@token)      
 end      
 else if(@Mode='logout')      
 Begin    
  Delete from User_Authorization where userId=@UserId and refreshToken=@RefreshToken and Token=@token      
 End      
end