self.addEventListener("push", event => { event.waitUntil(onPush(event)) });

async function onPush(event) {
	const push = event.data.json();
	console.log("push received", push)

	const { notification = {} } = { ...push };

	await self.registration.showNotification(notification.title, {
		body: notification.body,
	})
}