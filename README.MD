> [!CAUTION]
> This scheduled job was tested locally with a few test cases. Please review the code and test it in your environment before running it in production. Please also make sure you have a backup of your database before running this scheduled job.

# ℹ️ Delete Missing Commerce Properties Scheduled Job

This scheduled job will delete any missing commerce properties from the database. This is useful when you have removed a property from your code and you want to remove it from the database.

* **[Opti Addons] Delete missing Commerce data: List**
  * The one is read-only and will list all the missing properties.
* **[Opti Addons] Delete missing Commerce data: Delete**
  * The other one will delete all the missing properties.

# 🚀 Installation

Just install the package and the scheduled jobs will be available in the list of scheduled jobs.

If the version of your .NET or CMS/Commerce packages are different, you may either:

* Fork the code and update the references.
* Just copy and paste the code into your solution.

# 📃 Version history

* 0.1.0
  * Initial version