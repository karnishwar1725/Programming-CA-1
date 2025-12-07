import socket

HOST = "127.0.0.1"
PORT = 6000

print("=== DBS Admission Application Client ===")

name = input("Name: ")
address = input("Address: ")
qual = input("Educational qualifications: ")

print("\nCourses:")
print("1. MSc in Cyber Security")
print("2. MSc Information Systems & Computing")
print("3. MSc Data Analytics")

course_choice = input("Enter 1, 2 or 3: ")

if course_choice == "1":
    course = "MSc in Cyber Security"
elif course_choice == "2":
    course = "MSc Information Systems & Computing"
elif course_choice == "3":
    course = "MSc Data Analytics"
else:
    print("Invalid choice, defaulting to MSc Data Analytics")
    course = "MSc Data Analytics"

year = input("Intended start year (e.g. 2025): ")
month = input("Intended start month (1-12): ")

to_send = "|".join([name, address, qual, course, year, month])

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    print(f"\nConnecting to server {HOST}:{PORT}...")
    s.connect((HOST, PORT))
    s.sendall(to_send.encode("utf-8"))

    reg_no = s.recv(1024).decode("utf-8")
    if reg_no.startswith("ERROR"):
        print("Server error:", reg_no)
    else:
        print("\nApplication submitted successfully.")
        print("Your registration number is:", reg_no)