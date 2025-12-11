import requests
from bs4 import BeautifulSoup  # used to decrypt HTML
import csv   # saves scraped data to CSV

HOTEL_URLS = [
    ("Kv's B&B", "http://127.0.0.1:9000/hotel1.html"),
    ("Castle House B&B", "http://127.0.0.1:9000/hotel2.html")  # created hotel websites and they listen at port 9000
]

def scrape(url, hotel_name):   # scrapes hotel webpage
    page = requests.get(url)
    soup = BeautifulSoup(page.text, "html.parser")

    rooms = []  # list to store room data
    
    for r in soup.select(".room-card"):  # finds room card in a page
        
        name = r.select_one(".room-name").get_text(strip=True)
        price = r.select_one(".room-price").get_text(strip=True)
        rooms.append([hotel_name, name, price]) 
    return rooms   # gets room name and price in one row and returns it 

all_rooms = []   # list to store all scraped room data 

for hotel_name, url in HOTEL_URLS:
    all_rooms.extend(scrape(url, hotel_name))

with open("hotel_prices.csv", "w", newline="", encoding="utf-8") as f:   # stores scraped data in CSV file
    writer = csv.writer(f)
    writer.writerow(["Hotel", "Room", "Price"])
    writer.writerows(all_rooms)

print("\n=== HOTEL PRICE DATA ===\n")   
with open("hotel_prices.csv", "r", encoding="utf-8") as f:
    for line in f:
        print(line.strip())    # returns final data to the user after saving in CSV