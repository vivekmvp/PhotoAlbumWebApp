# PhotoAlbumWebApp
A simple photo web application created in .Net 6 to demonstrate Cloud Storage, Web App and Keyvault

> #### Note:  You have to setup Azure Cloud Storage and Azure Keyvault as shown below for this web application to function properly.

---

## Create Azure Storage with defaults

**Run below 2 commands to create storage account**

#### Create Resource Group
```
az group create -n rg-photoalbum -l eastus
```

#### Create Storage Account in Azure
```
az storage account create `
 -g rg-photoalbum `
 -n saphotoalbumwebapp `
 -l eastus `
 --sku Standard_LRS
```


#### Create Storage Account - Container with public access to the images
```
az storage container create -n myimages --public-access blob
```


#### Paste following details in app.settings file
Note: You have to find connection string for your azure storage account by visiting **Storage Account > Access Keys**
![image](https://user-images.githubusercontent.com/30829678/179418136-801e1e52-4bbb-4687-b676-9c91efd5ed85.png)
```
"Storage": 
{
    "AccountName": "saphotoalbumwebapp",
    "ContainerName":  "myimages",
    "ConnStr": "DefaultEndpointsProtocol=https;AccountName=saphotoalbumwebapp;AccountKey=J7Muw5ND6JOzjasSeYozzBlYLeFdGkGurJj04crLJCTrCGzQMBMn8zLg7xjTetDLqLAyF2lNWwqc+ASt8KnvQA==;EndpointSuffix=core.windows.net"
}
```  



