# server_mac.py
import asyncio
import websockets
import sys
import termios
import tty
import signal

class KeyboardHandler:
    def __init__(self):
        self.old_settings = None
        
    def setup(self):
        self.old_settings = termios.tcgetattr(sys.stdin)
        tty.setraw(sys.stdin.fileno())
        
    def restore(self):
        if self.old_settings:
            termios.tcsetattr(sys.stdin, termios.TCSADRAIN, self.old_settings)

keyboard_handler = KeyboardHandler()

async def handle_client(websocket):
    print("\nClient connected. Use WASD keys to move, Q to quit")
    print("-" * 50)
    
    try:
        while True:
            key = await asyncio.get_event_loop().run_in_executor(None, sys.stdin.read, 1)
            
            match key.lower():
                case 'w':
                    await websocket.send("input_1")
                    print("\rMoving: FORWARD ", end="", flush=True)
                case 's':
                    await websocket.send("input_2")
                    print("\rMoving: BACKWARD", end="", flush=True)
                case 'a':
                    await websocket.send("input_3")
                    print("\rMoving: LEFT   ", end="", flush=True)
                case 'd':
                    await websocket.send("input_4")
                    print("\rMoving: RIGHT  ", end="", flush=True)
                case 'q':
                    print("\nQuitting...")
                    return
                case _:
                    await websocket.send("no_input")
                    print("\rStopped       ", end="", flush=True)
            
            await asyncio.sleep(0.05)  # Prevent input spam
            
    except websockets.exceptions.ConnectionClosed:
        print("\nClient disconnected")
    except Exception as e:
        print(f"\nError: {e}")

async def main():
    def signal_handler(sig, frame):
        print("\nShutting down server...")
        keyboard_handler.restore()
        sys.exit(0)
        
    signal.signal(signal.SIGINT, signal_handler)
    
    keyboard_handler.setup()
    
    try:
        async with websockets.serve(handle_client, "localhost", 8765) as server:
            print("\nServer started on ws://localhost:8765")
            print("Waiting for Unity client connection...")
            await asyncio.Future()
    finally:
        keyboard_handler.restore()

if __name__ == "__main__":
    try:
        asyncio.run(main())
    except KeyboardInterrupt:
        keyboard_handler.restore()
        print("\nServer stopped")
