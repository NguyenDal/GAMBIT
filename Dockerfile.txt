# Use the Unity Hub base image
FROM gableroux/unity3d:2022.3.20f1

# Set the working directory inside the container
WORKDIR /app

# Copy the current directory contents into the container at /app
COPY . /app

# Set environment variables (if any)
# ENV VARIABLE_NAME value

# Run Unity in batch mode to build the project
RUN /opt/Unity/Editor/Unity -batchmode -nographics -silent-crashes -logFile /dev/null -projectPath /app -buildTarget StandaloneLinux64 -quit

# Clean up the project
RUN rm -rf /root/.cache/unity3d
RUN rm -rf /app/Library
RUN rm -rf /app/Temp
RUN rm -rf /app/Logs
RUN rm -rf /app/obj

# Set the entry point for the Docker container
ENTRYPOINT ["/bin/bash"]
