using System.Net.WebSockets;
using System.Text;

using var ws = new ClientWebSocket();
await ws.ConnectAsync(new Uri("ws://localhost:5065/"), CancellationToken.None);

var buffer = new byte[256];
while (ws.State == WebSocketState.Open)
{
    var result = await ws.ReceiveAsync(buffer, CancellationToken.None);
    if (result.MessageType == WebSocketMessageType.Close)
        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
    else
        Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, result.Count));
}