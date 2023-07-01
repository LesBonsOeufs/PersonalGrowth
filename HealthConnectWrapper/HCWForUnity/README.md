Add this to your main gradle's dependencies:

    //FOR HCW
    implementation 'androidx.health.connect:connect-client:1.0.0-alpha11'

Add this to your main manifest:

    In <activity>'s <intent-filter>:

        <action android:name="androidx.health.ACTION_SHOW_PERMISSIONS_RATIONALE"/>

    In <activity>:

        <meta-data android:name="health_permissions" android:resource="@array/health_permissions" />

    In <manifest>:

        <!-- Check whether Health connect is installed or not -->
        <queries>
            <package android:name="com.google.android.apps.healthdata" />
        </queries>