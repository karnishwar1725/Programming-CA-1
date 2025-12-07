import socket
import sqlite3

HOST = "127.0.0.1"
PORT = 6000

conn = sqlite3.connect("applications.db")
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
""")
conn.commit()

def save_application(name, address, qual, course, year, month):
    cur.execute(
        "INSERT INTO applicants (name, address, qualifications, course, start_year, start_month) VALUES (?, ?, ?, ?, ?, ?)",
        (name, address, qual, course, int(year), int(month))
    )
    app_id = cur.lastrowid
    reg_no = f"DBS{year}-{app_id:04d}"
    cur.execute("UPDATE applicants SET registration_no = ? WHERE id = ?", (reg_no, app_id))
    conn.commit()
    return reg_no

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen()
    print(f"Server listening on {HOST}:{PORT}")

    while True:
        client, addr = s.accept()
        print("Client connected:", addr)
        with client:
            data = client.recv(1024).decode("utf-8")
            if not data:
                continue
            
            parts = data.split("|")
            if len(parts) != 6:
                client.sendall("ERROR: invalid data".encode("utf-8"))
                continue

            name, address, qual, course, year, month = parts
            reg_no = save_application(name, address, qual, course, year, month)

            client.sendall(reg_no.encode("utf-8"))
            print("Saved application for", name, "Reg No:", reg_no)