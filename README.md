# meatlocker
Meet Locker is a file storage and retrieval application.

## Storage
The user provides a password and the file, and the server puts the file into a ZIP file protected by the password.  The server generates a key and uses this key and the password to determine where to store the ZIP file.  The key is returned to the user.  

## Retrieval
The user provides the key and the password and the server looks for the ZIP file and returns its bytes to the user.  The user receives the ZIP file and uses the password to access the original file.  Once the client receives the ZIP file bytes, the server deletes the ZIP file.

## Security
Passwords and keys are not stored. \
With just a key, an actor could not find or open the ZIP file. \
Files are not stored any longer than needed for retrieval.
