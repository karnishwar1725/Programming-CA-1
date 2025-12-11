import socket    # socket is imported to create TCP connection
import sqlite3   # import Database

HOST = "127.0.0.1"   # local host
PORT = 6000    # server is listening in this port 

conn = sqlite3.connect("applications.db")  # Creates and sqlite DB file to store data in it 
cur = conn.cursor()
cur.execute("""
CREATE TABLE IF NOT EXISTS applicants (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    address TEXT,
    qualifications TEXT,
    course TEXT,
    start_year INTEGER,
    start_month INTEGER,
    registration_no TEXT
)
""") # table creation, table is created only once
conn.commit()

def save_application(name, address, qual, course, year, month):
    cur.execute(
        "INSERT INTO applicants (name, address, qualifications, course, start_year, start_month) VALUES (?, ?, ?, ?, ?, ?)",
        (name, address, qual, course, int(year), int(month))   # function to save applicants data into DB 
    )
    
    app_id = cur.lastrowid  # auto generates an application ID
    
    reg_no = f"DBS{year}-{app_id:04d}" # gives registration number based on user year and app ID 
    
    
    cur.execute("UPDATE applicants SET registration_no = ? WHERE id = ?", (reg_no, app_id))
    conn.commit()
    
    return reg_no # saves registration number to DB and returns it to client 

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:  # this starts server process
    s.bind((HOST, PORT))  # binds server to IP and port
    s.listen()  # listens for active connections in the mentioned port 
    print(f"Server listening on {HOST}:{PORT}")

    while True:
        client, addr = s.accept()   # accept incoming connection 
        print("Client connected:", addr)
        with client:
            data = client.recv(1024).decode("utf-8")
            if not data:
                continue   # receives data from client if nothing received it skips 
            
            parts = data.split("|")  # splits data to fields as seen in client
            if len(parts) != 6:   # needs 6 fields from client 
                client.sendall("ERROR: invalid data".encode("utf-8"))
                continue

            name, address, qual, course, year, month = parts
            reg_no = save_application(name, address, qual, course, year, month)  # save user data to DB and generates a Registration number

            client.sendall(reg_no.encode("utf-8"))
            print("Saved application for", name, "Reg No:", reg_no)  # Sends the registration number to client 