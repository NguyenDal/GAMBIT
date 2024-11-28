import asyncio
import websockets
import keyboard  # For detecting key presses


async def send_key_inputs(websocket):
    print("Client connected. Hold (wasd) to send movement input, press 'q' to quit.")
    try:
        while True:
            # Check if the 'w' key is pressed
            if keyboard.is_pressed("w"):
                await websocket.send("input_1")
                print("Forward")
                await asyncio.sleep(0.1)  # Throttle to avoid spamming messages
            elif keyboard.is_pressed("a"):
                await websocket.send("input_3")
                print("Left")
                await asyncio.sleep(0.1)
            elif keyboard.is_pressed("s"):
                await websocket.send("input_2")
                print("Backward")
                await asyncio.sleep(0.1)
            elif keyboard.is_pressed("d"):
                await websocket.send("input_4")
                print("Right")
                await asyncio.sleep(0.1)
            # Check if the 'q' key is pressed to quit
            elif keyboard.is_pressed("q"):
                print("Quit command received. Disconnecting client.")
                break
            else:
                await websocket.send("no_input")
                await asyncio.sleep(0.1)

    except websockets.exceptions.ConnectionClosed:
        print("Client disconnected")
    except Exception as e:
        print(f"Error in send_key_inputs: {e}")


async def handle_client(websocket):
    await send_key_inputs(websocket)


async def main():
    try:
        async with websockets.serve(handle_client, "localhost", 8765):
            print("Server started on ws://localhost:8765")
            await asyncio.Future()  # Run forever
    except Exception as e:
        print(f"Server error: {e}")


if __name__ == "__main__":
    asyncio.run(main())
