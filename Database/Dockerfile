# create docker file for postgresql database image 
# use postgresql image as base image
FROM postgres:latest
# set environment variable for postgresql database
ENV POSTGRES_USER postgres
ENV POSTGRES_PASSWORD postgres
ENV POSTGRES_DB postgres
# expose port 5432
EXPOSE 5432
# run postgresql database
CMD ["postgres"]