# InterProcessNotification
This project provides a very simple IPC implementation for communication across processes on Windows Platform using event based notification between Producer and Consumer of inter process data.

This code internally uses EventWaitHandle and MemoryMappedFile for notification and data sharing respectively.

# Dependencies
1. Production code is dependent on Newtonsoft.Json.
2. Unit Test code is dependent on NUnit.
