# azure-blob-storage-dotnet-app
Azure Blob Storage Dotnet WebApp


This documentation provides a detailed explanation of the Azure Blob Storage service in your .NET application. The service handles uploading, downloading, deleting, and updating files, including JSON content, in Azure Blob Storage. The implementation also leverages Azurite, a lightweight Azure Storage emulator, to simulate Azure Blob Storage locally using Docker.

What is Azure Blob Storage?
Azure Blob Storage is a scalable cloud storage solution from Microsoft Azure designed for storing large amounts of unstructured data, such as images, videos, and documents. It's widely used in applications that require reliable and secure data storage.

What is Azurite?
Azurite is an open-source Azure Storage emulator that simulates Azure Blob, Queue, and Table services locally. It's a helpful tool for development and testing without needing to interact with the real Azure cloud services. By using Azurite, you can run and test your Azure Blob Storage-related code locally.

Prerequisites
Before using this service, ensure you have the following:

Docker: Azurite runs as a Docker container, so you need Docker installed on your machine.
Azurite Docker Image: The azurite Docker image is used to emulate Azure Blob Storage locally.
Basic Knowledge of C# and .NET: Understanding the fundamentals of C# and .NET will help you follow along with the implementation.
Setting Up Azurite with Docker
To run Azurite locally, you need to set up the Docker container with the following configuration:

Image: mcr.microsoft.com/azure-storage/azurite:latest
Container Name: azure-blob-storage
Volumes: The local directory ./.containers/blob_storage/data is mapped to /data in the container, which stores the blob data.
Ports: Port 10000 on your local machine is mapped to port 10000 in the container, enabling access to the Blob service.
This configuration allows you to run Azurite locally and simulate Azure Blob Storage operations as if you were interacting with the actual Azure cloud service.

Key Features of the Service
Upload File: Upload files to Azure Blob Storage (or Azurite locally) by streaming the file data and specifying its content type. The file is stored with a unique identifier to ensure no conflicts.

Download File: Retrieve a file by its unique identifier. The service fetches the file and returns the content along with its metadata.

Delete File: Remove a file from storage by specifying its unique identifier. The service ensures the file is deleted only if it exists.

Handle JSON Content: Upload, download, and update JSON files. This feature is particularly useful for storing and managing customer-related data in JSON format.

Configuration
Connection Strings: When using Azurite, you will need to configure your application with the appropriate connection string for local development. Typically, this will be something like DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02x...;BlobEndpoint=http://localhost:10000/devstoreaccount1;.

Environment Settings: Ensure your environment is set up to switch between the local Azurite connection string during development and the actual Azure connection string for production.

ould give you a clear understanding of how to use the Azure Blob Storage service in your .NET project, leveraging Azurite for local development and testing.