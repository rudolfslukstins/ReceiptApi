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

1. Created a method to create a new Receipt.
2. Created a method to get all created receipts.
3. Created a method to get receipt by id.
4. Created a method to get filtered receipts by creation date range.
5. Created a method to filtered receipts by product name - finds all receipts containing an item with the name containing text.
6. Created a method to delete receipt by id.
