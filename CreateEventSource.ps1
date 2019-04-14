#Creates the event source used by the default configuration of the project
#Needed for the logging to the Windows event log to work
#Be sure to run as administrator
#See readme for more infomation
New-EventLog -source "CoreXplore App" -LogName "My Apps"
