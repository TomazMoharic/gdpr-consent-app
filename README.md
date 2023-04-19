# GDPR Consent Application

This application was created to showcase some development techniques available to me as a developer.

It is still a work in progress (not all techniques are used on all modules), but it should provide the basic requested functionality while upholding a certain level of code quality.

Application is a web API, connected to a local MySql database. Within the application, in the Database folder is a **create_tables.sql** file with the export of the database schema.

To run the application a local MySql database (with the above schema) and a functioning .NET7 development environment are required.  Once running, the OpenApi Swagger interface is available to test the application.

# Known Issues
Integration test are still not working as they should. The application and database successfully start in a docker container but there is a DI issue with populating the test database with the required schema.

# TODO
This application is far from finished, since it was created on a limited amount of time. Following is the list of TODOs that would move the application closer to completion.

### Features
1. Add description field to consents and update the functionality.
2. Add created_at to users_consents and update the functionality.
3. Add an option to filter users_consents by the their expiration - return only non-expired users_consents.

### Refactor
1. consents module - controller, service, repository need to refactored to better accommodate testing.
2. users_consents module  - controller, service, repository need to refactored to better accommodate testing.
3. Logging needs to be implemented to other modules, currently only users module is being logged.
4. Unit tests need to cover all the modules, currently covering only a portion of the users module.
5. Integration testing needs to be extended to all the modules, currently covering only a portion of the users module.
6. Documentation needs to be extended, currently covering only the consents module.

### Functionality
1. Deploy application on the cloud (Azure).
2. Logging to Elastic search.