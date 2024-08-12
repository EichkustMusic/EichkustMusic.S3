# EichkustMusic.S3
**ASP.Net Core S3 service**
## Configure service
1. Add lines to *appsettings.json*
```
"S3": {
  "AccessKey": ...,
  "SecretKey": ...,
  "ServiceUrl": ...
}
```
2. Add the service using a DI
```
builder.services.AddS3(IConfigurationManager configuration);
```