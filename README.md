Redis Analyzer CLI

A simple tool to detect Redis issues in seconds.

---

## GETTING STARTED

1. Download and extract the zip file.

2. Open terminal (Command Prompt / Bash).

3. Navigate to the folder.

4. Run the tool:

Windows:
RedisAnalyzer.exe --host=YOUR_HOST --port=6379

Mac/Linux:
./RedisAnalyzer --host=YOUR_HOST --port=6379

---

## AUTHENTICATION (OPTIONAL)

If your Redis requires authentication:

Password only:
--pass=YOUR_PASSWORD

Username + Password (Redis ACL):
--user=USERNAME --pass=PASSWORD

Example:
RedisAnalyzer --host=localhost --user=default --pass=123456

---

## EXAMPLE

RedisAnalyzer --host=localhost

---

## WHAT IT DOES

The tool analyzes your Redis instance and detects:

* High memory usage
* Risk of crashes
* Slow commands
* Too many connections
* Misconfiguration

---

## OUTPUT

You will see results like:

CRITICAL

* Memory >90% & noeviction → writes may fail

WARNING

* Slow commands detected

INFO

* No persistence configured

---

## NOTES

* The tool runs locally.
* No data is sent anywhere.
* Safe to use in production.

---

## TROUBLESHOOTING

1. Connection failed:

   * Check host and port
   * Check firewall / network

2. Authentication failed:

   * Verify username/password

3. Permission issues (Mac/Linux):
   Run:
   chmod +x RedisAnalyzer

---

## SUPPORT

If you have issues, feel free to contact.

---

## END
