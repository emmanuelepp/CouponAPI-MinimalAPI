## CouponAPI - This is a example of minimal API with .NET 7

How to run

1 - Configure your appsettings.json with your server credentials.
Example: 
Server=YOUR_SERVER; Database=YOUR_DATABASE;TrustServerCertificate=true;Trusted_Connection=True; MultipleActiveResultSets=True;

2 - PM Console:
add-migration AddCoupon\
update-database

3 - Run the project and test the API
