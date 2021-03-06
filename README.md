# CompaniesHouse.NET

A simple .NET client wrapper for CompaniesHouse API.

[![install from nuget](http://img.shields.io/nuget/v/CompaniesHouse.svg?style=flat-square)](https://www.nuget.org/packages/CompaniesHouse)
[![downloads](http://img.shields.io/nuget/dt/CompaniesHouse.svg?style=flat-square)](https://www.nuget.org/packages/CompaniesHouse)
[![Build status](https://ci.appveyor.com/api/projects/status/0pgf5s626c0ybyrx/branch/master?svg=true)](https://ci.appveyor.com/project/Liberis/companieshouse-net/branch/master)

## Getting Started

CompaniesHouse.NET can be installed via the package manager console by executing the following commandlet:

```powershell
PM> Install-Package CompaniesHouse
```

Once we have the package installed, we can then create a `CompaniesHouseSettings` with a ApiKey which can be created via the [CompaniesHouse API website](https://developer.companieshouse.gov.uk/developer/applications)

```csharp
var settings = new CompaniesHouseSettings(apiKey);
```

We need to now create a `CompaniesHouseClient` - passing in the settings that we've just created.

```csharp
var client = new CompaniesHouseClient(settings);
```

This is the object we'll use going forward with any interaction to the CompaniesHouse API.

## Usage

### Searching for a company

To search for a company, we first need to create a `CompanySearchRequest` with details of the search we require.

```csharp
var request = new CompanySearchRequest()
{
    Query = "Liberis",
    StartIndex = 10,
    ItemsPerPage = 10
};
```

We can then pass the request object in to the `SearchCompanyAsync` method and await on the task.

```csharp
var result = await client.SearchCompanyAsync(request);
```

### Getting a company profile

To get a company profile, we pass a company number in to the `GetCompanyProfileAsync` method and await on the task.

```csharp
var result = await client.GetCompanyProfileAsync("03977902");
```

If there was no match for that company number then `null` will be returned.

### Getting company officer list

To get a list of officers for a company, we pass a company number in to the `GetOfficersAsync` method and await on the task.

```csharp
var result = await client.GetOfficersAsync("03977902");
```

We can also pass in some optional parameters of `startIndex` and `pageSize` which will allow us to page the results.

```csharp
var result = await client.GetOfficersAsync("03977902", 10, 10);
```


## Contributing

1. Fork
1. Hack!
1. Pull Request
