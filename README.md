package needed:
- dotnet 6 sdk
- crtl + shift + p: chọn nuget: open nuget gallery
    * Cài microsoft.entityframeworkcore, microsoft.entityframeworkcore.sqlite theo đúng version của host
    * Kiểm tra version của host: terminal: dotnet --info -> kiếm phần host
- donet ef
    + vào đường link: https://www.nuget.org/packages/dotnet-ef/7.0.0-preview.1.22076.6
    + ở mục .net cli global, copy command vào clipboard, chình version cho phù hợp với host -> version 6.0.3 -> dotnet tool install --global dotnet-ef --version 6.0.3      
    + vào thư mục api trong terminal, dán command và enter
    + xem command: dotnet ef -h
        . database: dotnet ef database -h
    * tạo migrations: dotnet ef migrations add <Tên Migration> -o <Folder output: có thể lồng folder vs "/">
    * update db: dotnet ef database update
- dotnet new:
    + -h: tùy chọn tạo mới
    + classlib -o Core: Tạo project tên "Core"
- dotnet sln:
    add Core: thêm project Core vào solution (root)
- dotnet add reference ../Infrastructure (đang ở mức api): thêm chỉ mục csproj và project Infrastructure
- dotnet restore: để các chỉ mục được đăng ký và ở trạng thái available trong project


extensions needed:
- c#
- c# extension
- nuget package manager
- nuget gallery
- sql lite

settings:
- gõ vào phần search: private
    + chọn c# extension: private member prefix: gõ "_"
- gõ vào phần search: this
    + chọn c# extension và bỏ dấu tích
git:
- init: tạo repo

thao tác sql lite
- dotnet ef database drop -p Infrastructure -s API: chuyển db từ project api sang project infrasture
- dotnet ef migrations remove -p Infrastruture -s API: xóa migrations ở api và bỏ sang infrastructure (đây chỉ là entity - ko phải dữ liệu)
- dotnet ef migrations add InitialCreate -p Infrastructure -s API -o Data/Migrations: thêm migrations vào infrastruture/data/migrations
- Điều chỉnh migrations:

Chú ý: https://stackoverflow.com/questions/52311053/more-than-one-dbcontext-was-found

khi clone mới lại project:
- xóa hết file trong Infrastructure/Data/Migration
- dotnet ef database drop --StoreContext -p Infrastructure -s API
dotnet ef migrations add <Tên nào khác migrations cũ> -p Infrastructure -s API -o Data/Migrations
- chạy lại api

setup angular
- npm install -g @angular/cli@12

docker
- chạy file setup .yml
    + docker-compose up --detach