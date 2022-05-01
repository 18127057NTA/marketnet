
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
- cài StackExchange.Redis cho project Infrastructure
    + ctrl + shift + p: Nuget package manager: Add package
    + StackExchange.Redis -> Chọn StackExchange.Redis
    + Chọn Infrastructure project


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
- dotnet ef migrations add InitialCreate(Có thể đặt tên khác) -p Infrastructure -s API -c <Tên của class Context> -o <Tên folder ms trong Infra>/Migrations
- dotnet ef database update -p Infrastructure -s API: update db(tất tả?) theo migration hiện có
- dotnet ef database update --context <Tên context> -p Infrastructure -s API
  --connection <CONNECTION>              The connection string to the database. Defaults to the one specified in AddDbContext or OnConfiguring.
  -c|--context <DBCONTEXT>               The DbContext to use. // CHỌN CONTEXT CỤ THỂ
  -p|--project <PROJECT>                 The project to use. Defaults to the current working directory.
  -s|--startup-project <PROJECT>         The startup project to use. Defaults to the current working directory.
  --framework <FRAMEWORK>                The target framework. Defaults to the first one in the project.
  --configuration <CONFIGURATION>        The configuration to use.
  --runtime <RUNTIME_IDENTIFIER>         The runtime to use.
  --msbuildprojectextensionspath <PATH>  The MSBuild project extensions path. Defaults to "obj".
  --no-build                             Don't build the project. Intended to be used when the build is up-to-date.
  -h|--help                              Show help information
  -v|--verbose                           Show verbose output.
  --no-color                             Don't colorize output.
  --prefix-output                        Prefix output with level.

dotnet ef migrations add VnvcInitialCreate -p Infrastructure -s API -o Data/Vnvc/Migrations
- Điều chỉnh migrations:

Chú ý: https://stackoverflow.com/questions/52311053/more-than-one-dbcontext-was-found

khi clone mới lại project:
- xóa hết file trong Infrastructure/Data/Migration
- dotnet ef database drop --StoreContext -p Infrastructure -s API
dotnet ef migrations add <Tên nào khác migrations cũ> -p Infrastructure -s API -o {Tên folder có thể cũ hoặc tạo mới}/Migrations
- chạy lại api

setup angular
- npm install -g @angular/cli@12
<<<<<<< Updated upstream
chạy angular
- ng serve

tải mongodb driver 2.15.0 trên nuget gallery cho project api
=======

docker
- chạy file setup .yml
    + docker-compose up --detach
>>>>>>> Stashed changes

Nhúng html string vào component trong angular: https://reactgo.com/angular-render-html/

REGEX để filter input trong client
https://www.regexlib.com/Default.aspx

Id để null vẫn tự generate (mongodb)
https://stackoverflow.com/questions/42349063/use-objectid-generatenewid-or-leave-mongodb-to-create-one

thêm dropdown menu trong angular:
 + https://stackoverflow.com/questions/68329468/angular-12-select-dropdown-option-based-on-the-value-in-template
 + https://stackoverflow.com/questions/34736161/create-a-dropdown-component

autoincremt primary key in EF:
https://stackoverflow.com/questions/61119828/auto-increment-primary-key-with-entity-framework

update entity(row):
https://entityframework.net/knowledge-base/46657813/how-to-update-record-using-entity-framework-core-
https://stackoverflow.com/questions/7149943/entity-framework-update-a-row-in-a-table
https://stackoverflow.com/questions/28932621/best-way-to-update-an-entity-in-entity-framework
https://stackoverflow.com/questions/42507640/update-specific-field-in-mongodb-document

chú ý về primary tự tăng:
https://stackoverflow.com/questions/61119828/auto-increment-primary-key-with-entity-framework

Xóa lịch sử shell: 
Remove-Item (Get-PSReadlineOption).HistorySavePath
Set-PSReadlineOption -HistorySaveStyle SaveNothing

Nối chuỗi
https://docs.microsoft.com/en-us/dotnet/csharp/how-to/concatenate-multiple-strings

Lấy kết quả trả về dạng Observable:https://stackoverflow.com/questions/61340819/convert-observableobject-to-string