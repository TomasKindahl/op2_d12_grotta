using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace op2_d12_grotta
{
    public class RoomState
    {
        public int self;
        public string name;
        public string image;
        public string trigger;
        public int next;
        public RoomState(int self, string name, string image, string trigger, int next)
        {
            this.self = self;
            this.name = name;
            this.image = image;
            this.trigger = trigger;
            this.next = next;
        }
    }
    public class Directions
    {
        public int north, east, south, west;
        public Directions(int N, int E, int S, int W)
        {
            north = N; east = E; south = S; west = W;
        }
    }
    public class Room
    {
        public static int NoDoor = -997;
        public int self;
        public string title;
        public string text;
        public string image;
        public List<RoomState> state;
        public int situation;
        Directions direction;
        //public Room(int self, string name, string text, string image, int N, int E, int S, int W, List<RoomState> state)
        public Room(int self, string name, string text, string image, Directions direction, List<RoomState> state)
        {
            this.self = self; this.title = name; this.text = text; this.image = image;
            // TODO: since all four directions makes the constructor ugly, class Directions should replace this:
            this.direction = direction;
            // TODO: the plan is to make the state[situation] responsible for drawing the room
            this.state = state;
            this.situation = 0;
        }
        public string Title { get { return title; } }
        public string Text { get { return text; } }
        public int North { get { return direction.north; } }
        public int East { get { return direction.east; } }
        public int South { get { return direction.south; } }
        public int West { get { return direction.west; } }
        public string Directions
        {
            get
            {
                string dir = "Det går dörrar till:\n";
                Directions D = direction;
                if (D.north != NoDoor)
                {
                    dir += "  w - norr";
                    if (D.north < 0) dir += " (stängd)"; dir += "\n";
                }
                if (D.east != NoDoor)
                {
                    dir += "  d - öster";
                    if (D.east < 0) dir += " (stängd)"; dir += "\n";
                }
                if (D.south != NoDoor)
                {
                    dir += "  s - söder";
                    if (D.south < 0) dir += " (stängd)"; dir += "\n";
                }
                if (D.west != NoDoor)
                {
                    dir += "  a - väster";
                    if (D.west < 0) dir += " (stängd)"; dir += "\n";
                }
                return dir;
            }
        }
        public string Image
        {
            get
            {
                return state[situation].image;
            }
        }
        public void UnLock()
        {
            Directions D = direction;
            if (D.north != NoDoor && D.north < 0)
            {
                D.north = -D.north;
            }
            if (D.south != NoDoor && D.south < 0)
            {
                D.south = -D.south;
            }
            if (D.east != NoDoor && D.east < 0)
            {
                D.east = -D.east;
            }
            if (D.west != NoDoor && D.west < 0)
            {
                D.west = -D.west;
            }
        }
    }
    public class Labyrinth
    {
        static string imgDir = "..\\..\\..\\images\\";
        static string warning = "";
        static Room help = new Room(-1, "Help",
               "Följande kommandon finns:\n" +
               "  w - gå genom dörren norrut\n" +
               "  s - gå genom dörren söderut\n" +
               "  d - gå genom dörren österut\n" +
               "  a - gå genom dörren västerut\n" +
               "  l - leta\n" +
               "  o - öppna låsta dörrar\n" +
               "  h - hjälp\n" +
               "  z - avsluta\n",
               "key-found.png",
               new Directions(Room.NoDoor, Room.NoDoor, Room.NoDoor, Room.NoDoor),
               new List<RoomState>());
        static List<Room> labyrinth = new List<Room>() {
            new Room(0, "Start",
                "Du står i ett rum med rött\n" +
                "tegel. Väggarna fladdrar i\n" +
                "facklornas sken. Du ser en\n" +
                "hög med tyg nere till vänster. ",
                "z-ingang-stangd-m-brate.png",
                new Directions(N:-3, E:Room.NoDoor, S:Room.NoDoor, W:Room.NoDoor),
                new List<RoomState>() {
                    new RoomState(0,"dörren är låst, skräp till\nvänster", "z-ingang-stangd-m-brate.png", "l", 1),
                    new RoomState(1,"dörren är låst", "z-ingang-stangd.png", "o", 2),
                    new RoomState(2,"dörren är öppen", "z-ingang-oppen.png", "s", 2),
                }
                ),
            new Room(1, "Lagerrum väst",
                "Du står i ett rum utan vägar\n" +
                "framåt. Du ser en hög med\n" +
                "skräp nere till vänster.",
                "z-lagerrum-vast.png",
                new Directions(N:Room.NoDoor, E:2, S:Room.NoDoor, W:Room.NoDoor),
                new List<RoomState>() {
                    new RoomState(0,"skräp nere till vänster.", "z-lagerrum-vast.png", "", 1),
                }
                ),
            new Room(2, "Vaktrum väst",
                "Du står i ett övergivet vaktrum.",
                "z-vaktrum-vast.png",
                new Directions(N:Room.NoDoor, E: 3, S:Room.NoDoor, W:1),
                new List<RoomState>() {
                    new RoomState(0,"", "z-vaktrum-vast.png", "", 1),
                }
                ),
            new Room(3, "Korsvägen",
                "Du står i korsväg. Det går\n" +
                "gångar i alla riktningar.",
                "z-korsvag-stangt-v-e.png",
                new Directions(N:6, E:4, S:0, W:2),
                new List<RoomState>() {
                    new RoomState(0,"dörrarna är låsta", "z-korsvag-stangt-v-e.png", "o", 1),
                    new RoomState(1,"dörren är låst", "z-korsvag-stangt-v.png", "o", 2),
                    new RoomState(2,"dörren är öppen", "z-korsvag-oppet.png", "s", 2)
                }),
            new Room(4, "Vaktrum öst",
                "Du står i ett övergivet vaktrum.",
                "z-vaktrum-ost-m-kista.png",
                new Directions(N:Room.NoDoor, E:5, S:Room.NoDoor, W:3),
                new List<RoomState>()),
            new Room(5, "Lagerrum öst",
                "Du står i ett rum utan vägar\n" +
                "framåt. Du ser en hög med\n" +
                "skräp nere till vänster.",
                "z-lagerrum-ost.png",
                new Directions(N:7, E:Room.NoDoor, S:Room.NoDoor, W:4),
                new List<RoomState>()),
            new Room(6, "Bron",
                "Du står vid en bro, som\n" +
                "går över en hög klyfta, som\n" +
                "du inte ser botten på.",
                "z-bro.png",
                new Directions(N:8, E:Room.NoDoor, S:3, W:Room.NoDoor),
                new List<RoomState>()),
            new Room(7, "Inre rummet",
                "Du står i ett rum med\n" +
                "bråte överallt.",
                "z-inre-rum-ost-m-brate.png",
                new Directions(N:Room.NoDoor, E:Room.NoDoor, S:5, W:Room.NoDoor),
                new List<RoomState>()),
            new Room(8, "Bortre rummet",
                "Du står i ett rum med\n" +
                "en kista i högra hörnet.",
                "z-nordrum-stängd-m-kista.png",
                new Directions(N : Room.NoDoor, E : Room.NoDoor, S : 6, W : -9),
                new List<RoomState>()),
            new Room(9, "Magiskt rum",
                "Du står i ett rum där en\n" +
                "magiker kastar en trollformel.",
                "z-trollisrum-m-trollis-2.png",
                new Directions(N : Room.NoDoor, E : 0, S : Room.NoDoor, W : Room.NoDoor),
                new List<RoomState>())
        };
        static int current = 0;
        public static string HelpTitle() { return help.Title; }
        public static string HelpText() { return help.Text; }
        public static void GoNext(ref int current, int next)
        {
            warning = "";
            if (next == Room.NoDoor) warning = "du gick in i väggen!\n";
            else if (next < 0) warning = "du gick in i en låst dörr!\n";
            else if (next >= labyrinth.Count) warning = "det känns som om du svävar\npå moln!\n";
            else current = next;
        }
        public static void DoCommand(string command)
        {
            // FIXME: try-catch? för om ett rum inte finns!
            int situation = labyrinth[current].situation;
            if (labyrinth[current].state[situation].trigger == command)
            {
                labyrinth[current].situation++;
            }
            if (command == "w")
            {
                GoNext(ref current, labyrinth[current].North);
            }
            else if (command == "s")
            {
                GoNext(ref current, labyrinth[current].South);
            }
            else if (command == "d")
            {
                GoNext(ref current, labyrinth[current].East);
            }
            else if (command == "a")
            {
                GoNext(ref current, labyrinth[current].West);
            }
            else if (command == "l")
            {
                warning = "du hittade ingenting\n";
            }
            else if (command == "o")
            {
                labyrinth[current].UnLock();
            }
            else if (command == "p")
            {
                warning = "";
            }
            else
            {
                warning = "okänt kommando\n";
            }
        }
        public static string CurrentTitle()
        {
            return labyrinth[current].Title;
        }
        public static string CurrentText()
        {
            return labyrinth[current].Text;
        }
        public static string WarningText()
        {
            return warning;
        }
        public static string Directions()
        {
            return labyrinth[current].Directions;
        }
        public static ImageSource GetImage()
        {
            BitmapFrame img = BitmapFrame.Create(new Uri(imgDir + labyrinth[current].Image, UriKind.RelativeOrAbsolute));
            return img;
        }
    }
}
