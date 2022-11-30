# PawsDay

## Get Start
* 確認appSetting或UserSecret連線字串
* 進行資料庫連線
```=
add-migration update -project Infrastructure -StartupProject PawsDay -OutputDir Data/Migrations
update-database -project Infrastructure -StartupProject PawsDay
```
## ConnectionString List
* PawsDay
    * Database
    * Redis
    * Cloudinary
    * SendGrid
    * LINE-Login
    * Google-Login
    * LINE-Bot
    * Azure-Language
* PawsDayBackStage
    * Database
    * JWT
    * Redis
    * SendGrid
    * Cloudinary