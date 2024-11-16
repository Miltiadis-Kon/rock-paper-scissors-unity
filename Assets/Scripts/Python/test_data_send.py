import socket
import time

# UDP server settings
UDP_IP = "127.0.0.1"  # IP address of the Unity application
UDP_PORT = 8000       # Port number the Unity app is listening on

# Create a UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# Function to send a string over UDP
def send_udp_string(message):
    byte_msg = message.encode()  # Encode the string to bytes
    sock.sendto(byte_msg, (UDP_IP, UDP_PORT))
    print(f"Sent message: {message}")

# Example usage
while True:
    send_udp_string("Hello from Python!")
    time.sleep(1)  # Send a new message every second