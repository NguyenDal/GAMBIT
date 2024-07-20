# Use the Unity CI image
FROM unityci/editor:2021.1.28f1-base-0.15.0

# Set the working directory inside the container
WORKDIR /project

# Copy the entire project directory into the container
COPY . /project

# Run the Unity build command with additional debug information
RUN /opt/unity/Editor/Unity -quit -batchmode -nographics -projectPath /project -logFile /project/unity_build.log -buildWindows64Player /project/Builds/Windows/myproject.exe || cat /project/unity_build.log
