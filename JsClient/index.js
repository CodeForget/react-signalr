let connection = $.hubConnection('http://localhost:6050');
let chatHubProxy = connection.createHubProxy('chatHub');
chatHubProxy.connection.start();
chatHubProxy.on('addNewMessageToPage', function (name, message) {
    console.log(`${name}: ${message}`);
});