# Notes on Instant

## GUI

I set up the angular sample project cause it's easier to generate it at the beginning and just make a few changes later than setting it up from scratch.
... But I didn't have time to set it up. Please ignore the angular part of the projet

## Heroku

I didn't have time to set up the project on heroku, but it could be done. Even though Heroku doesn't natively support .net core, there's a build pack for it (and it works, I use it at work)  
https://github.com/jincod/dotnetcore-buildpack

## Notes on the web API

* it uses Asp .Net Core 2.1
* it uses in memory database with EF Core (so it can be easily switch to a proper relational database)
* uses built in .Net Core DI
* at a high level, it only consist of 3 layers: a API layer, a business layer (AccountService and CardNumberGenerator), and a data layer

## Limitations

* the card number generator will be the bottle neck when scaling horizontally as it has to be a singleton to ensure no duplicate numbers are circulating (see comments in code)