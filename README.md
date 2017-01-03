# PluralsightDownloaderExtension
***Fiddler*** extension for downloading watched ***pluralsight course videos***. I have a relatively slower internet at home so need offline availability to revisit videos. My experience with offline downloader of pluralsight was not very good.

#Why a separate downloader:
I have a working pluralsight subscription and also used the offline downloader provided by them. Its limit of 30 courses annoys me a lot and i can not use on my mobile because of limited space. 


#How it Works:
- This allows to save only videos which I have watched and does not scrap pluralsight website. 
- It saves the videos in a directory structure for me and there is ***no limit for number of downloads***.


#How To Use:
- Build the project and paste the output dll in C:\Program Files (x86)\Fiddler4\Scripts or the scripts folder on your computer.


#Notes:
- It creates a new thread for downloading so do not go on a frenzy, else you might get blocked. 
- Just watch the pluralsight videos as you would and leave the rest to this extension.

***There is no need to store your credentials*** etc. in it but just provide folder path for downloading.

Future: Updated to load from settings or xml file.

Please report bugs that you find in this code or any objections.
Apache 2.0 License
