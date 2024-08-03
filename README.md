# Customer-Management
Hướng dẫn khởi tạo:

-Bước 1: 
Thay đổi chuỗi kết nối ở appsettings.json sao cho phù hợp với SQL Server ở localhost.

-Bước 2:
Mở terminal chạy lệnh :
	
	dotnet ef database update

-Bước 3: Có thể dùng bản backup trong thư mục Backups để restore trong SQL Server

-Bước 4: Chạy project test với Swagger UI