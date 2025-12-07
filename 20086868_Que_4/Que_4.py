import requests
from bs4 import BeautifulSoup
import csv

HOTEL_URLS = [
    ("Kv's B&B", "http://127.0.0.1:9000/hotel1.html"),
    ("Castle House B&B", "http://127.0.0.1:9000/hotel2.html")
]

def scrape(url, hotel_name):
    page = requests.get(url)
    soup = BeautifulSoup(page.text, "html.parser")

    rooms = []
    for r in soup.select(".room-card"):
        name = r.select_one(".room-name").get_text(strip=True)
        price = r.select_one(".room-price").get_text(strip=True)
        rooms.append([hotel_name, name, price])
    return rooms

all_rooms = []
for hotel_name, url in HOTEL_URLS:
    all_rooms.extend(scrape(url, hotel_name))

with open("hotel_prices.csv", "w", newline="", encoding="utf-8") as f:
    writer = csv.writer(f)
    writer.writerow(["Hotel", "Room", "Price"])
    writer.writerows(all_rooms)

print("\n=== HOTEL PRICE DATA ===\n")
with open("hotel_prices.csv", "r", encoding="utf-8") as f:
    for line in f:
        print(line.strip())