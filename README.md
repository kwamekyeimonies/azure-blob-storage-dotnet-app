# azure-blob-storage-dotnet-app
Azure Blob Storage Dotnet WebApp

This service handles file operations in Azure Blob Storage, such as uploading, downloading, deleting, and updating files. It also includes functionality for managing JSON files, like storing and retrieving customer settings. The service is designed for use in a .NET environment and can be tested locally using Azurite.

- What You Need
    - Azure Account: If you're working with actual Azure Blob Storage.
    - Azurite: A local emulator for Azure Blob Storage. Useful for development and testing without needing a real Azure account.
    - Docker: Required to run Azurite locally.

- Key Concepts
  - Blob Storage: A place in Azure where you can store large amounts of unstructured data (e.g., files, backups).
  - Containers: Think of them as folders in Blob Storage that organize your files (blobs).
  - Blobs: The actual files stored in Blob Storage.
  
# Features
  - Upload File: Upload a file to Azure Blob Storage, generating a unique ID for each file.
  - Download File: Retrieve a file from storage using its unique ID.
  - Delete File: Remove a file from storage.
  - Upload JSON: Store JSON data (like customer settings) in a .json file.
  - Download JSON: Retrieve JSON data from storage and convert it back into its original format.
  - Update JSON: Replace the contents of an existing JSON file with updated data.