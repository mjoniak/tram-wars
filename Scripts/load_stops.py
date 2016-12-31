import xml.etree.ElementTree as ET
import urllib.request as req
import codecs

BASE_URL = "https://www.openstreetmap.org/api/0.6"

def load_stops(line_number, direction, line_ref):
    print(f"Loading {line_number}_{direction}...")
    xml_text = req.urlopen(f"{BASE_URL}/relation/{line_ref}").read().decode("utf-8")
    root = ET.fromstring(xml_text)
    members = root.findall(".//member")
    stop_nodes = [m for m in members if m.get("role") in ("stop", "stop_entry_only", "stop_exit_only")]
    stop_refs = [node.get("ref") for node in stop_nodes]
    with codecs.open(f"Scripts/Output/{line_number}_{direction}.csv", "w", "utf-8") as out:
        for stop_ref in stop_refs:
            stop_xml = req.urlopen(f"{BASE_URL}/node/{stop_ref}").read().decode("utf-8")
            stop_root = ET.fromstring(stop_xml)
            stop_node = stop_root.find("./node")
            lat, lon = stop_node.get("lat"), stop_node.get("lon")
            name = stop_node.find("./tag[@k='name']").get("v")
            out.write(f"{name},{lat},{lon}\n")

if __name__ == "__main__":
    for line in open("Scripts/stops.csv"):
        line_number, id1, id2 = line.strip().split(",")
        load_stops(line_number, "a", id1)
        load_stops(line_number, "b", id2)
        