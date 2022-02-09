# SearchFight
C# Console Application to compare how many search results they return for a list of terms.

## Prerequisites

-   .NET 5 SDK

## How To Run

1. Put your API keys in `appsettings.json` file.

```javascript
"BingSearchSettings": {
    "ApiKey": "[BING_API_KEY]"
  },
"GoogleSearchSettings": {
    "ApiKey": "[GOOGLE_API_KEY]",
    "Cx": "[GOOGLE_CONTEXT_PARAMETER]"
  }
```

2. Open terminal and run the .NET Project with your terms. For example:
```bash
dotnet run java .net
```

## Supported Search Engines

Add aditional search engines by creating interface and class inherit form IEngineSearchService.

-   Bing Search
-   Google

## Technologies Used

-   .NET 5
-   Google Custom Search API
-   Bing API
-   xUnit
-   Visual Studio 2022
