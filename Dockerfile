FROM docker/dev-environments-default:stable-1
RUN wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN sudo dpkg -i packages-microsoft-prod.deb
RUN rm packages-microsoft-prod.deb
RUN sudo apt-get update && \
sudo apt-get install -y dotnet-sdk-7.0