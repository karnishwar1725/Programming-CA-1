import socket  # socket is imported to create TCP connection

HOST = "127.0.0.1" # local host
PORT = 6000   # server is listening in this port 

print("=== DBS Admission Application Client ===")

name = input("Name: ")
address = input("Address: ")
qual = input("Educational qualifications: ")  # user enters their details

print("\nCourses:")
print("1. MSc in Cyber Security")
print("2. MSc Information Systems & Computing")
print("3. MSc Data Analytics")  

course_choice = input("Enter 1, 2 or 3: ")   # user selects course

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
month = input("Intended start month (1-12): ")   # gets year and month details

to_send = "|".join([name, address, qual, course, year, month])   # combines all fiels into one string so that its easy for server to read the fields

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:   # create TCP Connection
    print(f"\nConnecting to server {HOST}:{PORT}...")
    s.connect((HOST, PORT))   # Connect to server
    s.sendall(to_send.encode("utf-8"))   # send applicant data in a encoded format

    reg_no = s.recv(1024).decode("utf-8")  # receive Reg. Number from server
    
    
    if reg_no.startswith("ERROR"):
        print("Server error:", reg_no)
    else:
        print("\nApplication submitted successfully.")
        print("Your registration number is:", reg_no)   # provides us with an registration number or ERROR message