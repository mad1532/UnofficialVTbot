# UnofficialVTbot

This is a discord bot written in c# using DSharpPlus. It uses the VirusTotal API to quickly display the results of a hash or a virustotal URL (as the website does not have a preview avialible in discord).

# Usage

```!VTscan <hash>```

```!VTembed <url of virustotal scan>```

#  Build + setup

build a dockerfile using Visual Studio targeted for linux. then use the cp command to copy in the text file with your Discord API and VirusTotal token to the /app directory. then run it.
