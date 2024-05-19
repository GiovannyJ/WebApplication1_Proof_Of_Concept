# WebApplication1_Proof_Of_Concept

## About
This is a proof of concept as to how we can implement an API arciteture into our system.
The API would run constantly and the endpoints would serve the CRUD operations of the tables.
In this example it is not fully fledged out, there are ways to make it more dynamic so that we can have 1 API for multiple databases, as of now this API servers 1 database with multiple tables (some tables are omitted for simplicty)



## Folders
* Controllers: API route endpoint and hanlders
* Database: Database driver and query generator
* Models: Data Structures (replicated from db tables)
* JSON_templates: Models as to how to format JSON body in request

