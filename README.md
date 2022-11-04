# ReceiptApi

## **To run project!**

To run the project you will need a Mircrosoft Visual Studio and SQLite. 
When the project is opened in Microsoft Visual Studio you should make a migration.

```
First : add-migration init
```
```
Then: : update-database
```
The database should be in WEB project root.

## **Description!**

For Receipt controller:

Created a method to create a new Receipt.
Created a method to get all created receipts.
Created a method to get receipt by id.
Created a method to get filtered receipts by creation date range.
Created a method to filtered receipts by product name - finds all receipts containing an item with the name containing text.
Created a method to delete receipt by id.
